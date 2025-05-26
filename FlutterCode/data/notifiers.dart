//valueNotifier : hold the data
//ValueListenableBuilder : listen to the data (dont need to set state)

import 'package:flutter/widgets.dart';

ValueNotifier<int> selectedPageNotifier = ValueNotifier(0);
ValueNotifier<bool> isDarkModeNotifier = ValueNotifier(true);
