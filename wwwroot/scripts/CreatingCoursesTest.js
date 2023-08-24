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

var discriminators = ["skip",1,"CoursePageAnswerFieldElement","CoursePageImageElement",4,"CoursePageTextElement"]

var selectedDisc = ""

var backGrounds = ["skip","../images/SlideTemplateFirst.png","../images/SlideTemplateSecond.png","../images/SlideTemplateThird.png"]

var checkMouseHold = false

var checkMouseHolding = false

var newID = 0

var newIDimg = 0

var currentId = "-1"

var currentIdImg = "-1"

var selectedBackG = ""

var slidesID = 2

form.addEventListener("click", function(e) {
    if(e.target.id < 6) {

        selectedDisc = discriminators[e.target.id]

    } 
    
    if (e.target.id == 5) {

        var copy = document.getElementById("textfield").cloneNode(true)
        var copyField = document.getElementById("inputfield").cloneNode(true)

        copy.id = "textfield." + newID
        copyField.id = "inputfield." + newID


        copy.style.cssText = "display: block;"
        copyField.style.cssText = "display: block;"
        
        copy.appendChild(copyField)

        document.getElementById("textfield").after(copy)

        document.getElementById(copy.id).onmousedown = function() {
            checkMouseHold = true
            let id = "" + copy.id
            currentId = id.split(".")[1]
        }

        newID += 1

    } 
    
    if (e.target.id == 1) {

        var parent = document.querySelector("#containerSlides")
        var div = document.createElement("div")

        div.className = "SlidePreview"
        div.id = "slidePreview" + slidesID
        div.style.cssText = "background: url(" + selectedBackG + "); background-size: 100% 100%"

        parent.appendChild(div)

        slidesID += 1

    } 
    
    if (e.target.id >= 6) {
        e.preventDefault()
        document.getElementById("slideMain").style.cssText = "background: url(" + backGrounds[e.target.id-5] + "); background-size: 100% 100%"
        document.getElementById("slidePreview1").style.cssText = "background: url(" + backGrounds[e.target.id-5] + "); background-size: 100% 100%"

        selectedBackG = backGrounds[e.target.id-5]

        var slidesPrev = document.querySelectorAll(".SlidePreview")

        slidesPrev.forEach((item) => {
            item.style.cssText = "background: url(" + backGrounds[e.target.id-5] + "); background-size: 100% 100%"
        })
    }

    if (e.target.id == 3) {

        var copyImg = document.getElementById("imagefield").cloneNode(true)
        var copyImgField = document.getElementById("image").cloneNode(true)

        copyImg.id = "imagefield." + newIDimg
        copyImgField.id = "image." + newIDimg


        copyImg.style.cssText = "display: block;"
        copyImgField.style.cssText = "display: block;"
        
        copyImg.appendChild(copyImgField)

        document.getElementById("imagefield").after(copyImg)

        document.getElementById(copyImg.id).onmousedown = function() {
            checkMouseHolding = true
            let id = "" + copyImg.id
            currentIdImg = id.split(".")[1]
        }

        newIDimg += 1
    }
})

document.getElementById("slideMain").onmousemove = function(e) {
    if (checkMouseHold) {
        if (e.pageX <= 1489 && e.pageY <= 857) {
            document.getElementById("inputfield." + currentId).style.cssText = "display: block; left: " + e.pageX + "px; top: " + e.pageY + "px;"
            document.getElementById("textfield." + currentId).style.cssText = "display: block; left: " + e.pageX + "px; top: " + e.pageY + "px;"
        }

        else if (e.pageX > 1489 && e.pageY > 857) {
            document.getElementById("inputfield." + currentId).style.cssText = "display: block; left: 1489px; top: 857px;"
            document.getElementById("textfield." + currentId).style.cssText = "display: block; left: 1489px; top: 857px;"
        }

        else if (e.pageX > 1489) {
            document.getElementById("inputfield." + currentId).style.cssText = "display: block; left: 1489px; top: " + e.pageY + "px;"
            document.getElementById("textfield." + currentId).style.cssText = "display: block; left: 1489px; top: " + e.pageY + "px;"
        }

        else if (e.pageY > 857) {
            document.getElementById("inputfield." + currentId).style.cssText = "display: block; left: " + e.pageX + "px; top: 857px;"
            document.getElementById("textfield." + currentId).style.cssText = "display: block; left: " + e.pageX + "px; top: 857px;"
        }
    }



    if (checkMouseHolding) {
        if (e.pageX <= 1489 && e.pageY <= 857) {
            document.getElementById("image." + currentIdImg).style.cssText = "display: block; left: " + e.pageX + "px; top: " + e.pageY + "px;"
            document.getElementById("imagefield." + currentIdImg).style.cssText = "display: block; left: " + e.pageX + "px; top: " + e.pageY + "px;"
        }

        else if (e.pageX > 1489 && e.pageY > 857) {
            document.getElementById("image." + currentIdImg).style.cssText = "display: block; left: 1489px; top: 857px;"
            document.getElementById("imagefield." + currentIdImg).style.cssText = "display: block; left: 1489px; top: 857px;"
        }

        else if (e.pageX > 1489) {
            document.getElementById("image." + currentIdImg).style.cssText = "display: block; left: 1489px; top: " + e.pageY + "px;"
            document.getElementById("imagefield." + currentIdImg).style.cssText = "display: block; left: 1489px; top: " + e.pageY + "px;"
        }

        else if (e.pageY > 857) {
            document.getElementById("image." + currentIdImg).style.cssText = "display: block; left: " + e.pageX + "px; top: 857px;"
            document.getElementById("imagefield." + currentIdImg).style.cssText = "display: block; left: " + e.pageX + "px; top: 857px;"
        }
    }
}

window.onmouseup = function() {
    checkMouseHold = false
    checkMouseHolding = false
}



// document.getElementById("containerSlides").onclick = function(e) {
//     if (Number(e.target.id.substr(-1,1)) > 0) {

//         var slides = document.querySelectorAll("#slideMain")

//         slides.forEach((item) =>{
//             item.style.cssText = "display: none; z-index: 1"
//         })

//         slides[Number(e.target.id.substr(-1,1))-1].style.cssText = "display: block; background: url(" + selectedBackG + "); z-index: 2; background-size: 100% 100%"
//     }
// }