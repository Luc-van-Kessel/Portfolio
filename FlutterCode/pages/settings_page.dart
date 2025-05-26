import 'package:flutter/material.dart';
import 'package:flutter_app/views/pages/expanded_flexible_page.dart';
import 'package:flutter_app/views/pages/jotform_page.dart';

class SettingsPage extends StatefulWidget {
  const SettingsPage({
    super.key,
    required this.title,
  });

  final String title;
  @override
  State<SettingsPage> createState() => _SettingsPageState();
}

class _SettingsPageState extends State<SettingsPage> {
  TextEditingController controller = TextEditingController();
  bool? isChecked = false;
  bool isSwitched = false;
  double sliderValue = 0.0;
  String? menuItem = 'e1';
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
        leading: BackButton(
          onPressed: () {
            Navigator.pop(context);
          },
        ),
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(20.0),
          child: Column(
            children: [
              ElevatedButton(
                onPressed: () {
                  ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(
                      content: Text("snackbar"),
                      duration: Duration(seconds: 2),
                      behavior: SnackBarBehavior.floating,
                    ),
                  );
                },
                child: Text('Open snackbar'),
              ),
              Divider(
                color: Colors.teal,
                thickness: 5.0,
              ),
              ElevatedButton(
                onPressed: () {
                  showDialog(
                    context: context,
                    builder: (context) {
                      return AlertDialog(
                        title: Text('Dialog'),
                        content: Text('This is a dialog'),
                        actions: [
                          FilledButton(
                            onPressed: () {
                              Navigator.pop(context);
                            },
                            child: Text('close'),
                          ),
                        ],
                      );
                    },
                  );
                },
                child: Text('Open Dialog'),
              ),
              DropdownButton(
                value: menuItem,
                items: [
                  DropdownMenuItem(
                    value: 'e1',
                    child: Text('Element One'),
                  ),
                  DropdownMenuItem(
                    value: 'e2',
                    child: Text('Element 2'),
                  ),
                  DropdownMenuItem(
                    value: 'e3',
                    child: Text('Element 3'),
                  ),
                ],
                onChanged: (String? value) {
                  setState(
                    () {
                      menuItem = value;
                    },
                  );
                },
              ),
              // always mention in pubspec.yaml
              TextField(
                controller: controller,
                decoration: InputDecoration(
                  border: OutlineInputBorder(),
                ),
                onEditingComplete: () {
                  setState(() {});
                },
              ),
              Text(controller.text),
              Checkbox(
                //tristate can have three values
                tristate: true,
                value: isChecked,
                onChanged: (bool? value) {
                  setState(() {
                    isChecked = value;
                  });
                },
              ),
              CheckboxListTile(
                tristate: true,
                title: Text('Open snackbar'),
                value: isChecked,
                onChanged: (bool? value) {
                  setState(() {
                    isChecked = value;
                  });
                },
              ),
              Switch(
                value: isSwitched,
                onChanged: (bool value) {
                  isSwitched = value;
                },
              ),
              SwitchListTile.adaptive(
                title: Text('Click me!'),
                value: isSwitched,
                onChanged: (value) {
                  setState(() {
                    isSwitched = value;
                  });
                },
              ),
              Slider.adaptive(
                value: sliderValue,
                onChanged: (double value) {
                  setState(() {
                    sliderValue = value;
                  });
                },
              ),
              InkWell(
                splashColor: Colors.amberAccent,
                onTap: () {
                  print('object;');
                },
                child: Container(
                  height: 200.0,
                  width: double.infinity,
                  color: Colors.black26,
                ),
              ),

              ElevatedButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    //display the page
                    MaterialPageRoute(
                      builder: (context) {
                        return ExpandedFlexiblePage();
                      },
                    ),
                  );
                },
                child: Text('Show flexible and expanded'),
              ),
              FilledButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    //display the page
                    MaterialPageRoute(
                      builder: (context) {
                        return JotformPage();
                      },
                    ),
                  );
                },
                child: Text('Show Jotform'),
              ),
              TextButton(
                onPressed: () {},
                child: Text('click me'),
              ),
              OutlinedButton(
                onPressed: () {},
                child: Text('click me'),
              ),
              CloseButton(),
              BackButton(),
            ],
          ),
        ),
      ),
    );
  }
}
