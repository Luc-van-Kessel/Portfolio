import 'package:flutter/material.dart';
import 'package:flutter_app/data/notifiers.dart';
import 'package:flutter_app/views/pages/welcome_page.dart';

class ProfilePage extends StatelessWidget {
  const ProfilePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.all(20.0),
      child: Column(
        children: [
          CircleAvatar(
            radius: 50.0,
            //you need to do it in reverse order
            // first you need to get the image from the asset
            backgroundImage:
                AssetImage('assets/images/Wallpaperbackground.jpg'),
          ),
          ListTile(
            title: Text('logout'),
            onTap: () {
              // dont forget to reset the selected page notifier
              selectedPageNotifier.value = 0;
              Navigator.pushReplacement(
                context,
                //display the page
                MaterialPageRoute(
                  builder: (context) {
                    return WelcomePage();
                  },
                ),
              );
            },
          )
        ],
      ),
    );
  }
}
