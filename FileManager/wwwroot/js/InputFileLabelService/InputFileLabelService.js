function setLabelValueForInputFile(elementReference, inputFileId, labelId) {
    if (elementReference.files.length > 0 && elementReference.files[0].size < 20000000) {
        document.getElementById(labelId).innerText = elementReference.files[0].name;
    } else if (elementReference.files[0].size > 20000000) {
        document.getElementById(inputFileId).value = ``;
        document.getElementById(labelId).innerText = `Выберите файл`;
        alert("Максимальный размер файла 20 Мб");
    }
    else {
        document.getElementById(inputFileId).value = ``;
        document.getElementById(labelId).innerText = `Выберите файл`
    }
}