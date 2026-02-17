var isTntWeightSliderMouseDown = false;

function AddTNTWeightSliderEvent() {

    document.getElementById("tnt-weight-slider").onmousedown = () => {
        isTntWeightSliderMouseDown = true;
    }

    document.onmouseup = () => {
        if (isTntWeightSliderMouseDown) {
            isTntWeightSliderMouseDown = false;
            DotNet.invokeMethod("PearlCalculatorBlazor", "ChangeTntWeightJs");
        }
    }
}