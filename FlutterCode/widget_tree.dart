import 'package:flutter/material.dart';
import 'package:flutter_app/data/constants.dart';
import 'package:flutter_app/data/notifiers.dart';
import 'package:flutter_app/views/pages/home_page.dart';
import 'package:flutter_app/views/pages/profile_page.dart';
import 'package:flutter_app/views/pages/settings_page.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../widgets/navbar_widget.dart';

List<Widget> pages = [
  HomePage(
    title: 'Home',
  ),
  ProfilePage(),
];

class WidgetTree extends StatelessWidget {
  const WidgetTree({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: Text('data'),
          centerTitle: true,
          actions: [
            IconButton(
              //await async always put before brackets
              onPressed: () async {
                isDarkModeNotifier.value = !isDarkModeNotifier.value;

                //await waits for the things before it to finish
                final SharedPreferences prefs =
                    await SharedPreferences.getInstance();
                await prefs.setBool(
                    KConstants.themeModeKey, isDarkModeNotifier.value);
              },
              icon: ValueListenableBuilder(
                valueListenable: isDarkModeNotifier,
                builder: (context, isDarkMode, child) {
                  return isDarkMode
                      ? Icon(Icons.dark_mode)
                      : Icon(Icons.light_mode);
                },
              ),
            ),
            IconButton(
              onPressed: () {
                // push it to a certain page
                Navigator.push(
                  context,
                  //display the page
                  MaterialPageRoute(
                    builder: (context) {
                      return SettingsPage(
                        title: 'Settings',
                      );
                    },
                  ),
                );
              },
              icon: Icon(Icons.settings),
            ),
          ],
        ),
        body: ValueListenableBuilder(
          valueListenable: selectedPageNotifier,
          builder: (context, value, child) {
            return pages.elementAt(value);
          },
        ),
        bottomNavigationBar: NavbarWidget());
  }
}
