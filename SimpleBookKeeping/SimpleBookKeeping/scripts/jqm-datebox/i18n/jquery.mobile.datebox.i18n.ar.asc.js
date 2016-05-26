/*
 * jQuery Mobile Framework : plugin to provide a date and time picker.
 * Copyright (c) JTSage
 * CC 3.0 Attribution.  May be relicensed without permission/notifcation.
 * https://github.com/jtsage/jquery-mobile-datebox
 *
 * Translation by: Michael de Lara <mykel1829@yahoo.com>
 *
 */

jQuery.extend(jQuery.mobile.datebox.prototype.options.lang, {
	'ar': {
		setDateButtonLabel: "&#1578;&#1593;&#1610;&#1610;&#1606; &#1578;&#1575;&#1585;&#1610;&#1582;",
		setTimeButtonLabel: "&#1590;&#1576;&#1591; &#1575;&#1604;&#1608;&#1602;&#1578;",
		setDurationButtonLabel: "&#1578;&#1593;&#1610;&#1610;&#1606; &#1575;&#1604;&#1605;&#1583;&#1577;",
		calTodayButtonLabel: "&#1575;&#1604;&#1602;&#1601;&#1586; &#1573;&#1604;&#1609; &#1575;&#1604;&#1610;&#1608;&#1605;",
		titleDateDialogLabel: "&#1575;&#1582;&#1578;&#1585; &#1575;&#1604;&#1578;&#1575;&#1585;&#1610;&#1582;",
		titleTimeDialogLabel: "&#1575;&#1582;&#1578;&#1585; &#1575;&#1604;&#1608;&#1602;&#1578;",
		daysOfWeek: ["&#1575;&#1604;&#1571;&#1581;&#1583;", "&#1575;&#1604;&#1575;&#1579;&#1606;&#1610;&#1606;", "&#1575;&#1604;&#1579;&#1604;&#1575;&#1579;&#1575;&#1569;", "&#1575;&#1604;&#1575;&#1585;&#1576;&#1593;&#1575;&#1569;", "&#1575;&#1604;&#1582;&#1605;&#1610;&#1587;", "&#1575;&#1604;&#1580;&#1605;&#1593;&#1577;", "&#1575;&#1604;&#1587;&#1576;&#1578;"],
		daysOfWeekShort: ["&#1575;&#1604;&#1571;&#1581;&#1583;", "&#1575;&#1604;&#1575;&#1579;&#1606;&#1610;&#1606;", "&#1575;&#1604;&#1579;&#1604;&#1575;&#1579;&#1575;&#1569;", "&#1575;&#1604;&#1575;&#1585;&#1576;&#1593;&#1575;&#1569;", "&#1575;&#1604;&#1582;&#1605;&#1610;&#1587;", "&#1575;&#1604;&#1580;&#1605;&#1593;&#1577;", "&#1575;&#1604;&#1587;&#1576;&#1578;"],
		monthsOfYear: ["&#1610;&#1606;&#1575;&#1610;&#1585;", "&#1601;&#1576;&#1585;&#1575;&#1610;&#1585;", "&#1605;&#1575;&#1585;&#1587;", "&#1573;&#1576;&#1585;&#1610;&#1604;", "&#1605;&#1575;&#1610;&#1608;", "&#1610;&#1608;&#1606;&#1610;&#1577;", "&#1610;&#1608;&#1604;&#1610;&#1577;", "&#1571;&#1594;&#1587;&#1591;&#1587;", "&#1587;&#1576;&#1578;&#1605;&#1576;&#1585;", "&#1571;&#1603;&#1578;&#1608;&#1576;&#1585;", "&#1606;&#1608;&#1601;&#1605;&#1576;&#1585;", "&#1583;&#1610;&#1587;&#1605;&#1576;&#1585;"],
		monthsOfYearShort: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
		durationLabel: ["&#1571;&#1610;&#1575;&#1605;", "&#1587;&#1575;&#1593;&#1577;", "&#1583;&#1602;&#1610;&#1602;&#1577;", "&#1579;&#1575;&#1606;&#1610;&#1577;"],
		durationDays: ["&#1610;&#1608;&#1605;", "&#1571;&#1610;&#1575;&#1605;"],
		tooltip: "&#1601;&#1578;&#1581; &#1605;&#1606;&#1578;&#1602;&#1610; &#1575;&#1604;&#1578;&#1575;&#1585;&#1610;&#1582;",
		nextMonth: "&#1575;&#1604;&#1578;&#1575;&#1604;&#1610;",
		prevMonth: "&#1575;&#1604;&#1587;&#1575;&#1576;&#1602;",
		timeFormat: 24,
		headerFormat: '%A, %B %-d, %Y',
		dateFieldOrder: ['d','m','y'],
		timeFieldOrder: ['h', 'i', 'a'],
		slideFieldOrder: ['y', 'm', 'd'],
		dateFormat: "%d/%m/%Y",
		useArabicIndic: true,
		isRTL: true,
		calStartDay: 0,
		clearButton: "&#1608;&#1575;&#1590;&#1581;&#1577;",
		durationOrder: ['d', 'h', 'i', 's'],
		meridiem: ["AM", "PM"],
		timeOutput: "%k:%M",
		durationFormat: "%Dd %DA, %Dl:%DM:%DS",
		calDateListLabel: "&#1578;&#1608;&#1575;&#1585;&#1610;&#1582; &#1571;&#1582;&#1585;&#1609;",
		calHeaderFormat: "%B %Y"
	}
});
jQuery.extend(jQuery.mobile.datebox.prototype.options, {
	useLang: 'ar'
});

