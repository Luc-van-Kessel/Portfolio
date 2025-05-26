import 'package:flutter/material.dart';
import 'package:flutter_app/views/widget_tree.dart';
import 'package:flutter_app/widgets/hero_widget.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key, required this.title});

  final String title;

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  TextEditingController controllerEmail = TextEditingController(text: '123');
  TextEditingController controllerPw = TextEditingController(text: '456');
  String confirmedEmail = '123';
  String confirmedPw = '456';

//overrides
  @override
  void dispose() {
    controllerEmail.dispose();
    controllerPw.dispose();

    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    // this is used to get the screen size
    double widthScreen = MediaQuery.of(context).size.width;

    return Scaffold(
      appBar: AppBar(),
      body: Center(child: LayoutBuilder(
        builder: (context, constraints) {
          // this operator is used to check if the screen is small or large
          // more useful if you want to use it for ipad and phone
          return FractionallySizedBox(
            widthFactor: widthScreen > 600 ? 0.5 : 1.0,
            child: SingleChildScrollView(
              child: Padding(
                padding: const EdgeInsets.all(20.0),
                child: Column(
                  children: [
                    HeroWidget(
                      title: widget.title,
                    ),
                    SizedBox(height: 20.0),
                    TextField(
                      controller: controllerEmail,
                      decoration: InputDecoration(
                        hintText: 'Email',
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(15.0),
                        ),
                      ),
                      onEditingComplete: () {
                        setState(() {});
                      },
                    ),
                    SizedBox(height: 10.0),
                    TextField(
                      controller: controllerPw,
                      decoration: InputDecoration(
                        hintText: 'Password',
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(15.0),
                        ),
                      ),
                      onEditingComplete: () {
                        setState(() {});
                      },
                    ),
                    SizedBox(height: 10.0),
                    FilledButton(
                      onPressed: () {
                        onLoginPressed();
                      },
                      style: FilledButton.styleFrom(
                          minimumSize: Size(double.infinity, 20.0)),
                      child: Text(widget.title),
                    ),
                  ],
                ),
              ),
            ),
          );
        },
      )),
    );
  }

// how you write a function and it needs a return type
  void onLoginPressed() {
    // if the email and password are correct
    // this is an if statement
    if (confirmedEmail == controllerEmail.text &&
        confirmedPw == controllerPw.text) {
      Navigator.pushAndRemoveUntil(
        context,
        //display the page
        MaterialPageRoute(
          builder: (context) {
            return WidgetTree();
          },
        ),
        (route) => false,
      );
    }
  }
}
