import 'package:flutter/material.dart';
import 'package:webview_flutter/webview_flutter.dart';

class JotformPage extends StatefulWidget {
  const JotformPage({super.key});

  @override
  State<JotformPage> createState() => _JotformPageState();
}

class _JotformPageState extends State<JotformPage> {
  WebViewController controller = WebViewController()
    ..setJavaScriptMode(JavaScriptMode.unrestricted)
    ..setNavigationDelegate(
      NavigationDelegate(
        onProgress: (int progress) {
          // Update loading bar.
        },
        onPageStarted: (String url) {},
        onPageFinished: (String url) {},
        onHttpError: (HttpResponseError error) {},
        onWebResourceError: (WebResourceError error) {},
        onNavigationRequest: (NavigationRequest request) {
          if (request.url.startsWith('https://www.youtube.com/')) {
            return NavigationDecision.prevent;
          }
          return NavigationDecision.navigate;
        },
      ),
    )
    ..loadRequest(Uri.parse('https://form.jotform.com/250672969917070'));
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Jotform')),
      body: WebViewWidget(controller: controller),
    );
  }
}
