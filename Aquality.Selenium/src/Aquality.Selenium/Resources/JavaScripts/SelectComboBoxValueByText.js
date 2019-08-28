function selectValue(element, text) {
    for (var i = 0; i < element.options.length; i++) {
        if (element.options[i].text == text) {
            element.options[i].selected = true;
        }
    }
    if ("createEvent" in document) {
        var evt = document.createEvent("HTMLEvents");
        evt.initEvent("change", false, true);
        element.dispatchEvent(evt);
    } else {
        element.fireEvent("onchange");
    }
}
selectValue(arguments[0], arguments[1]);
