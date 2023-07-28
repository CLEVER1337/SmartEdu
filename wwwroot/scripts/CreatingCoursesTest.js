// document.getElementById('slideMain').onclick = async function(e) {
//     var rect = e.target.getBoundingClientRect();
//     var x = e.clientX - rect.left;
//     var y = e.clientY - rect.top;
//     if(x >= 0 && y >= 0 && selectedDisc !== "") {
//         await fetch("../course/element/create", {
//             method: "POST",
//             headers: {"Accept": "application/json", "Content-Type": "application/json"},
//             body: JSON.stringify({
//               Discriminator: selectedDisc,
//               CoursePageId: 0,
//               CourseId: 0,
//               Coords: x + ";" + y
//             })
//         })
//     }
//     selectedDisc = ""
// }

var form = document.querySelector("form")

var discriminators = [1,"CoursePageAnswerFieldElement","CoursePageImageElement",4,"CoursePageTextElement"]

var selectedDisc = ""

var backGrounds = ["1","2","3"]

var checkMouseHold = false

var newID = ""

form.addEventListener("click", function(e) {
    if(e.target.id < 5) {
        selectedDisc = discriminators[e.target.id]
    } if (e.target.id == 4) {
        var newTextField = document.getElementById("TextField").cloneNode()
        newTextField.id = "TextField" + newID
        var newInputField = document.getElementById("inputfield").cloneNode()
        newInputField.id = "inputfield" + newID
        document.getElementById("inputfield" + newID).style.cssText = "display: block;"
        newInputField.style.cssText = "display: block;"
        document.getElementById("TextField" + newID).onmousedown = function() {
            checkMouseHold = true
        }
        newTextField.onmousedown = function() {
            checkMouseHold = true
        }
        document.getElementById("slideMain").onmousemove = function(e) {
            if (checkMouseHold) {
                document.getElementById("inputfield" + newID).style.cssText = "display: block; left: " + e.pageX + "px; top: " + e.pageY + "px;"
            }
        }
        document.getElementById("slideMain").onmouseup = function() {
            checkMouseHold = false
            newID += 1
        }
    } else {
        e.preventDefault()
        document.getElementById("slideBackG").src = backGrounds[e.target.id-5]
    }
})

