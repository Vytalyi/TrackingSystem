$(document).ready(function () {
	$('input[type=checkbox]').click(function () {
		var selector = $(this).attr('id');
		if (!$(this).is(':checked'))
			$('td.' + selector + ',th.' + selector).hide(300);
		else
			$('td.' + selector + ',th.' + selector).show(300);
	})
	$('#ShowHideFilterBtn').click(function () {
		var selector = $('.filter-list');
		if (selector.css('display') == 'none')
			selector.show(300);
		else
			selector.hide(300);
	})
})