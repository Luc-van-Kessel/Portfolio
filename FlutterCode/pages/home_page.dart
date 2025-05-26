import 'package:flutter/material.dart';
import 'package:flutter_app/data/constants.dart';
import 'package:flutter_app/views/pages/course_page.dart';
import 'package:flutter_app/widgets/containter_widget.dart';
import 'package:flutter_app/widgets/hero_widget.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key, required this.title});

  final String title;
  @override
  Widget build(BuildContext context) {
    List<String> list = [
      KValue.keyConcepts,
      KValue.baiscLayout,
      KValue.cleanUI,
      KValue.fixBugs
    ];
    return SingleChildScrollView(
      child: Padding(
        padding: EdgeInsets.symmetric(horizontal: 10.0),
        child: Column(
          children: [
            HeroWidget(
              title: 'home',
              nextPage: CoursePage(),
            ),
            ...List.generate(
              list.length,
              (index) {
                return ContainterWidget(
                  title: list.elementAt(index),
                  description: 'description $index',
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
