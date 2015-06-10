// When the calculator loads, hide the result display elements and make them visible.
//   Ensures that the result elements don't even appear for an instant on startup.
function onCalculatorLoad() {
    $("#resultDisplayElmnts").hide();
    document.getElementById("resultDisplayElmnts").style.visibility = "visible";
}

// New numbers are added to the formula only when an operand is clicked, so
//    store the pressed numbers until an operand is clicked
var newNumber = "0";
var calcDone = false;
var CLEARED_FORMULA = "- - - - - -"

function updateCalcFormula(newInput) {
    if (calcDone) {
        document.getElementById("formulaText").innerHTML = CLEARED_FORMULA;
        hideResultsDisplay();

        calcDone = false;
    }

    // If a digit, immediately append to text of new number or set
    //   new number as this digit if already 0
    if (/\d/.test(newInput) || newInput == ".") {
        if (newNumber == "0")
            newNumber = newInput;
        else
            newNumber += newInput;
    }
    // If the positive/negative indicator, set the sign
    //   of newNumber as appropriate (if 0 then ignore)
    else if (newInput == "+/-") {
        if (newNumber != "0") {
            if (/-/.test(newNumber))
                newNumber = newNumber.substr(1);
            else
                newNumber = "-" + newNumber;
        }
    }
    // If the input is the backspace, then remove one digit from
    //   newNumber or set to 0
    else if (newInput == String.fromCharCode("8592")) {
        if (newNumber.length == 1)
            newNumber = "0";
        else
            newNumber = newNumber.substr(0, newNumber.length - 1);
    }
    // If an operand, add newNumber and the operand to the formula
    else {
        var currentFormula = document.getElementById("formulaText").innerHTML;

        if (currentFormula == CLEARED_FORMULA)
            document.getElementById("formulaText").innerHTML = newNumber + " " + newInput + " ";
        else
            document.getElementById("formulaText").innerHTML += newNumber + " " + newInput + " ";

        newNumber = "0";
    }

    document.getElementById("newNumberText").innerHTML = newNumber;
}

// Resets the calculator to default starting values and display
function clearFormula() {
    document.getElementById("formulaText").innerHTML = "- - - - - -";
    document.getElementById("newNumberText").innerHTML = "0";
    newNumber = "0";

    hideResultsDisplay();

    calcDone = false;
}

// Translates the formula into numbers and operands and performs the calculation.
//    Only allows pressing '=' once.
function performCalc() {
    if (!calcDone) {
        if (document.getElementById("formulaText").innerHTML == CLEARED_FORMULA) {
            document.getElementById("formulaText").innerHTML = newNumber;
            document.getElementById("resultText").innerHTML = newNumber;
            showResultsDisplay();
        }
        else {
            document.getElementById("formulaText").innerHTML += newNumber;
            newNumber = "0";

            showResultsDisplay();

            var completeFormula = document.getElementById("formulaText").innerHTML;

            $.post("/Calculator/ProcessInputFormula",
                        { inputFormula: completeFormula },
                        function (result) {
                            $("#resultText").html(result);
                        });
        }

        calcDone = true;

    }
}

// Hides the newNumber display and shows the result display
function showResultsDisplay() {
    $("#newNumberText").hide(500);
    $("#resultDisplayElmnts").show(500);
}

// Hides the result display and returns the newNumber display
function hideResultsDisplay() {
    $("#newNumberText").show(500);
    $("#resultDisplayElmnts").hide(500);
}