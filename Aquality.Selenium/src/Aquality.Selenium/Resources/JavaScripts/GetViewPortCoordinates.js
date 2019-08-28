return (function (element) {
    let rect = element.getBoundingClientRect();
    return [rect.left, rect.top];
})(arguments[0]);
