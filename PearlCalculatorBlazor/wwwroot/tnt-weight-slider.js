function AddTNTWeightSliderEvent() {
    document.getElementById("tnt-weight-slider").onmouseup = () => {
        DotNet.invokeMethod("PearlCalculatorBlazor", "ChangeTNTWeightJS");
    };
}