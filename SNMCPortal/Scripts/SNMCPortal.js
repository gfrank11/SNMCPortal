$(document).ready(function () {
    $('.draggable-menu-item').each(function (i) {
        $(this).draggable({
            cursor: 'move',
            helper: 'clone'
        });
    });
    //The jQuery constructor accepts a 2nd parameter which can be used to override the context of the selection.
    //jQuery("img", this);
    $('.quadrant').each(function (i) {
        var itemId = '#' + this.id;
        $(itemId).droppable({
            activeClass: "ui-state-default",
            hoverClass: "ui-drop-hover",
            over: function (event, ui) {
                var $this = $(this);
            },
            drop: function (event, ui) {
                var draggable = ui.draggable;
                var targetUrl = $('a', draggable).attr('href');
                var quadrant = $(this).attr('id');
                var url = '@Url.Action("RegisterQuadrant","Home")';
                $('#' + quadrant).load("Home/RegisterQuadrant", { quadrant: quadrant, targetUrl: targetUrl });
            }
        }); //droppable
    }); //each
    $('.menu-link').click(function (e) {
        e.preventDefault();
        if ($(this).hasClass('inactive'))  //don't use 'disabled' because jquery ui keyes on it and does its own thing
            return false;
        enableMenuItem();
        var clickedLink = $(this);
        $('#main-content').fadeOut(500, function () {
            $('#main-content').children().first().remove();
            var url = clickedLink.attr('href');
            $('#main-content').load(url, function () {
                $('#main-content').fadeIn(500);
                disableMenuItem(clickedLink);
            });
        });
        return false;
    });
});  //ready
function enableMenuItem() {
    $('.menu-link.inactive').css({cursor:'default', color:'White'});
    $('.menu-link.inactive')
    $('.menu-link.inactive').removeClass('inactive');
}
function disableMenuItem(item) {
    item.addClass('inactive');
    $('.menu-link.inactive').css({color:'#5f9ea0'});
    $('.menu-link.inactive:hover').css({cursor:'not-allowed'});
}
//for debugging
function isProcessed(section) {
    alert(section + " is processed");
}