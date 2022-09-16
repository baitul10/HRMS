if (document.readyState) {
    console.log("HRM initial");
}

//const btnDivision = document.getElementById('btn-division');
//const btnDepartment = document.getElementById('btn-department');
//const btnSection = document.getElementById('btn-section');

//btnDivision.addEventListener('click', function () {
//    addRowToTable('division');
//});

//btnDepartment.addEventListener('click', function () {
//    addRowToTable('department');
//});

//btnSection.addEventListener('click', function () {
//    addRowToTable('section');
//});


function addRowToTable(pName) {
    const divEl = document.getElementById(pName);
    let elTable = divEl.getElementsByTagName('table')[0];
    let elTableRows = elTable.rows;
    var rowLenth = elTableRows.length;
    let rowOuterHtml = elTableRows[elTableRows.length - 1].outerHTML;
    rowOuterHtml = rowOuterHtml.replaceAll('_' + Number(rowLenth - 2) + '_', '_' + Number(rowLenth - 1) + '_');
    rowOuterHtml = rowOuterHtml.replaceAll('[' + Number(rowLenth - 2) + '].', '[' + Number(rowLenth - 1) + '].');
    let newRow = elTable.insertRow();
    newRow.innerHTML = rowOuterHtml;
    ClearCreatedRow(pName);
}



function ClearCreatedRow(pName) {
    const targetedDiv = document.getElementById(pName);
    let table = targetedDiv.getElementsByClassName("table")[0];
    let rows = table.rows;
    var lastRow = rows[rows.length - 1];
    var tableTd = lastRow.getElementsByTagName("td");

    for (var i = 0; i < tableTd.length - 1; i++) {
        var lastChildofTd = tableTd[i].getElementsByTagName("input")[0];
        if (i === 0) {
            lastChildofTd.value = "0";
        } else {
            lastChildofTd.value = "";
        }
    }
}

