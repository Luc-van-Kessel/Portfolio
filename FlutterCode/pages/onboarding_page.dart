import 'package:flutter/material.dart';
import 'package:flutter_app/data/constants.dart';
import 'package:flutter_app/views/pages/login_page.dart';
import 'package:lottie/lottie.dart';

class OnboardingPage extends StatelessWidget {
  const OnboardingPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(),
      body: Center(
        child: SingleChildScrollView(
          child: Padding(
            padding: const EdgeInsets.all(20.0),
            child: Column(
              children: [
                Lottie.asset('assets/lotties/hey.json', height: 300.0),
                SizedBox(height: 20.0),
                Text(
                  'wassup this screen is cool huh ?',
                  style: KTextStyle.descriptionText,
                  textAlign: TextAlign.justify,
                ),
                SizedBox(height: 10.0),
                FilledButton(
                  onPressed: () {
                    Navigator.push(
                      context,
                      //display the page
                      MaterialPageRoute(
                        builder: (context) {
                          return LoginPage(
                            title: 'Register',
                          );
                        },
                      ),
                    );
                  },
                  style: FilledButton.styleFrom(
                      minimumSize: Size(double.infinity, 20.0)),
                  child: Text('next'),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
