function SetCurrentTab(tab) {
    var cur = $('.menu').find('.current');
    if (cur.length > 0)
        cur.removeClass('current');

    if (tab == 'issues')
        $('#IssuesTab').addClass('current');
    else if (tab == 'search')
        $('#SearchTab').addClass('current');
}