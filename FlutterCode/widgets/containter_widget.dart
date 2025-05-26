import 'package:flutter/material.dart';
import 'package:flutter_app/data/constants.dart';

class ContainterWidget extends StatelessWidget {
  const ContainterWidget(
      {super.key, required this.title, required this.description});

  final String description;
  final String title;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      padding: EdgeInsets.only(top: 0.0),
      child: Card(
        margin: EdgeInsets.zero,
        child: Padding(
          padding: EdgeInsets.all(20.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                title,
                style: KTextStyle.titleTealText,
              ),
              Text(description, style: KTextStyle.descriptionText),
            ],
          ),
        ),
      ),
    );
  }
}
