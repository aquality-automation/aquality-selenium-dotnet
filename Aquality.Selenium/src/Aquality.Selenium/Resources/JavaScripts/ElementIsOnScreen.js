return (function (element) {
    let rect = element.getBoundingClientRect();
    return rect.top >= 0 && rect.left >= 0
        && (rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) && rect.bottom !== 0)
        && (rect.right <= (window.innerWidth || document.documentElement.clientWidth) && rect.right !== 0);
})(arguments[0]);
