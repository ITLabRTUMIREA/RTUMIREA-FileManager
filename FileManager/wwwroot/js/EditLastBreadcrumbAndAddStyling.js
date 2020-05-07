let lastElOfList = document.getElementsByClassName("breadcrumbs").item(0).childNodes.item(
    document.getElementsByClassName("breadcrumbs").item(0).childNodes.length - 1);
let innerText = lastElOfList.textContent;
lastElOfList.textContent = "";
let newInner = document.createElement("a");
newInner.className = "breadcrumb-active";
newInner.innerText = innerText;
lastElOfList.appendChild(newInner);
