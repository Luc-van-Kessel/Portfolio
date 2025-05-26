import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:flutter_app/classes/activity_class.dart';
import 'package:http/http.dart' as http;

class CoursePage extends StatefulWidget {
  const CoursePage({super.key});

  @override
  CoursePageState createState() => CoursePageState();
}

class CoursePageState extends State<CoursePage> {
  bool isFirst = true;

  Future<Activity> fetchActivity() async {
    final response =
        await http.get(Uri.parse('https://bored-api.appbrewery.com/random'));
    if (response.statusCode == 200) {
      return Activity.fromJson(jsonDecode(response.body));
    } else {
      throw Exception('Failed to load activity');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Activity Viewer'),
        actions: [
          IconButton(
            icon: Icon(Icons.ads_click),
            onPressed: () {
              setState(() {
                isFirst = !isFirst;
              });
            },
          ),
        ],
      ),
      body: Center(
        child: FutureBuilder<Activity>(
          future: fetchActivity(),
          builder: (context, snapshot) {
            if (snapshot.connectionState == ConnectionState.waiting) {
              return const CircularProgressIndicator();
            } else if (snapshot.hasError) {
              return Text('Error: ${snapshot.error}');
            } else if (!snapshot.hasData) {
              return const Text('No data available');
            }

            final activity = snapshot.data!;
            return Padding(
              padding: const EdgeInsets.all(16.0),
              child: AnimatedCrossFade(
                firstChild: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text('Activity: ${activity.activity}',
                        style: const TextStyle(
                            fontSize: 18, fontWeight: FontWeight.bold)),
                    Text('Type: ${activity.type}'),
                    Text('Participants: ${activity.participants}'),
                    Text('Price: \$${activity.price.toStringAsFixed(2)}'),
                    Text('Availability: ${activity.availability}'),
                    Text('Accessibility: ${activity.accessibility}'),
                    Text('Duration: ${activity.duration}'),
                    Text(
                        'Kid Friendly: ${activity.kidFriendly ? "Yes" : "No"}'),
                    InkWell(
                      onTap: () => launchURL(activity.link),
                      child: Text('More Info',
                          style: const TextStyle(
                              color: Colors.blue,
                              decoration: TextDecoration.underline)),
                    ),
                  ],
                ),
                secondChild: Center(
                  child: Image.asset('assets/images/Wallpaperbackground.jpg'),
                ),
                crossFadeState: isFirst
                    ? CrossFadeState.showFirst
                    : CrossFadeState.showSecond,
                duration: Duration(seconds: 4),
              ),
            );
          },
        ),
      ),
    );
  }

  void launchURL(String url) async {
    // Use url_launcher package to open links
  }
}
