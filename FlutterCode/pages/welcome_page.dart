import 'package:flutter/material.dart';
import 'package:flutter_app/views/pages/login_page.dart';
import 'package:flutter_app/views/pages/onboarding_page.dart';
import 'package:lottie/lottie.dart';

class WelcomePage extends StatelessWidget {
  const WelcomePage({super.key, req});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: SingleChildScrollView(
          child: Padding(
            padding: const EdgeInsets.all(20.0),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Lottie.asset('assets/lotties/bomb.json', height: 300.0),
                FittedBox(
                  child: Text(
                    'FLUTTER APP',
                    style: TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: 200.0,
                        letterSpacing: 500.0),
                  ),
                ),
                SizedBox(height: 20.0),
                FilledButton(
                  onPressed: () {
                    Navigator.push(
                      context,
                      //display the page
                      MaterialPageRoute(
                        builder: (context) {
                          return OnboardingPage();
                        },
                      ),
                    );
                  },
                  style: FilledButton.styleFrom(
                      minimumSize: Size(double.infinity, 20.0)),
                  child: Text('Get Started'),
                ),
                TextButton(
                  onPressed: () {
                    Navigator.push(
                      context,
                      //display the page
                      MaterialPageRoute(
                        builder: (context) {
                          return LoginPage(
                            title: 'login',
                          );
                        },
                      ),
                    );
                  },
                  style: FilledButton.styleFrom(
                      minimumSize: Size(double.infinity, 20.0)),
                  child: Text('Login'),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
