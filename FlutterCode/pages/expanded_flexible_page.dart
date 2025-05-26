import 'package:flutter/material.dart';

class ExpandedFlexiblePage extends StatelessWidget {
  const ExpandedFlexiblePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Expanded and Flexible Test'),
      ),
      body: Column(
        //expanded takes all the space available in the parent widget , collumn takes vertically
        // and row takes horizontally
        // and if there is a other expanded widget in the same parent widget it will take the remaining space
        children: [
          Row(
            children: [
              Expanded(
                child: Container(
                  color: Colors.red,
                  height: 20,
                  child: Text('hello'),
                ),
              ),
              Flexible(
                child: Container(
                  color: Colors.green,
                  height: 20,
                  //the flexible widget does not take the whole space if there is text inside the widget and will look how long the string is
                  child: Text('hello'),
                ),
              ),
            ],
          ),
          Divider(),
          Row(
            children: [
              Flexible(
                child: Container(
                  color: Colors.green,
                  height: 20,
                  //the flexible widget does not take the whole space if there is text inside the widget and will look how long the string is
                  child: Text('hello'),
                ),
              ),
              Expanded(
                child: Container(
                  color: Colors.red,
                  height: 20,
                  child: Text('hello'),
                ),
              ),
            ],
          )
        ],
      ),
    );
  }
}

/// DONT PUT IN SINGECHILD SCROLL VIEW OR LIST VIEW OR ANY SCROLLABLE WIDGETS
/// ELSE IT WILL BE INFINITE HEIGHT AND WILL CRASH THE APP
