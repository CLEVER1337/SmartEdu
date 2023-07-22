document.getElementById('ID блока').onclick = async function(e) {
    var rect = e.target.getBoundingClientRect();
    var x = e.clientX - rect.left;
    var y = e.clientY - rect.top;
    if(x >= 0 && y >= 0) {
        await fetch("../course/element/create", {
            method: "POST",
            headers: {"Accept": "application/json", "Content-Type": "application/json"},
            body: JSON.stringify({
              Discriminator: selectedDisc,
              CoursePageId: 0,
              CourseId: 0,
              Coords: x + ";" + y
            })
        })
    }
} 

var form = document.querySelector("form")

var discriminators = [1,"CoursePageAnswerFieldElement","CoursePageImageElement",4,"CoursePageTextElement"]

var selectedDisc

form.onclick = function(e) {
    selectedDisc = discriminators[e.target.id]
}

