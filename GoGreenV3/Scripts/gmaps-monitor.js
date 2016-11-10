function initMarkerArray(id, type, status, latitude, longitude, working, controllable, c) {
    markersArray[c] = new Array();
    markersArray[c] = [id, type, status, latitude, longitude, working, controllable, '', false, c];

    m[c] = new Array();
    m[c] = [id, type, '', 0, working, controllable];
}

var ctr = (function () {
    var ctr = 0;
    return function () {
        return ctr++;
    }
})();