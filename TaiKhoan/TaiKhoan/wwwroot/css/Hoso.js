document.addEventListener('DOMContentLoaded', function () {
    var menuItems = document.querySelectorAll('.menu-hoso > ul > li > a');
    menuItems.forEach(function (item) {
        item.addEventListener('click', function (event) {
            var parentLi = event.target.parentElement;
            parentLi.classList.toggle('open');
            var subMenu = parentLi.querySelector('.sub-menu');
            if (subMenu) {
                subMenu.style.display = subMenu.style.display === 'flex' ? 'none' : 'flex';
            }
        });
    });
});
