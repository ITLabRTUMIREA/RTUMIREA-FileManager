function setLabelValueForInputFile(elementReference, labelId) {
    if (elementReference.files.length > 0) {
        document.getElementById(labelId).innerText = elementReference.files[0].name;
    }
    else {
        document.getElementById(labelId).innerText = `Выберите файл`
    }
}