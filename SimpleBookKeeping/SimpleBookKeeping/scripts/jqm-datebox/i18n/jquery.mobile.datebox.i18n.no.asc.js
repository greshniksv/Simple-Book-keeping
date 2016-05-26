/*
 * jQuery Mobile Framework : plugin to provide a date and time picker.
 * Copyright (c) JTSage
 * CC 3.0 Attribution.  May be relicensed without permission/notifcation.
 * https://github.com/jtsage/jquery-mobile-datebox
 *
 * Translation by: Unknown
 *
 */

jQuery.extend(jQuery.mobile.datebox.prototype.options.lang, {
	'no': {
		setDateButtonLabel: "Velg dato",
		setTimeButtonLabel: "Velg tidspunkt",
		setDurationButtonLabel: "Velg varighet",
		calTodayButtonLabel: "G&#229; til dagens dato",
		titleDateDialogLabel: "Velg dato",
		titleTimeDialogLabel: "Velg tidspunkt",
		daysOfWeek: ["S&#248;ndag", "Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag", "L&#248;rdag"],
		daysOfWeekShort: ["S&#248;n", "Man", "Tirs", "Ons", "Tors", "Fre", "L&#248;r"],
		monthsOfYear: ["Januar", "Februar", "Mars", "April", "Mai", "Juni", "Juli", "August", "September", "Oktober", "November", "Desember"],
		monthsOfYearShort: ["Jan", "Feb", "Mar", "Apr", "Mai", "Jun", "Jul", "Aug", "Sep", "Okt", "Nov", "Des"],
		durationLabel: ["Dager", "Timer", "Minutter", "Sekunder"],
		durationDays: ["Dag", "Dager"],
		tooltip: "&#197;pne datovelger",
		nextMonth: "Neste m&#229;ned",
		prevMonth: "Forrige m&#229;ned",
		timeFormat: 24,
		headerFormat: '%A, %B %-d, %Y',
		dateFieldOrder: ['d','m','y'],
		timeFieldOrder: ['h', 'i', 'a'],
		slideFieldOrder: ['y', 'm', 'd'],
		dateFormat: "%m/%d/%Y",
		useArabicIndic: false,
		isRTL: false,
		calStartDay: 0,
		clearButton: "Klart",
		durationOrder: ['d', 'h', 'i', 's'],
		meridiem: ["AM", "PM"],
		timeOutput: "%k:%M",
		durationFormat: "%Dd %DA, %Dl:%DM:%DS",
		calDateListLabel: "Andre datoer",
		calHeaderFormat: "%B %Y"
	}
});
jQuery.extend(jQuery.mobile.datebox.prototype.options, {
	useLang: 'no'
});

