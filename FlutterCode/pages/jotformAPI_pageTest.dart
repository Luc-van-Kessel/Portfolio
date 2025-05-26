import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:flutter/material.dart';

Future<void> getJotformForms() async {
  const apiKey = 'YOUR_JOTFORM_API_KEY';
  final url = Uri.parse('https://api.jotform.com/user/forms?apiKey=$apiKey');

  final response = await http.get(url);

  if (response.statusCode == 200) {
    final data = json.decode(response.body);
    print('Your forms: ${data['content']}');
  } else {
    print('Failed to load forms. Status code: ${response.statusCode}');
  }
}
