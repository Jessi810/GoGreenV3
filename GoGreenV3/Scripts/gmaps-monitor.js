var monitor = [], pos = [], stat = [], info = [];
var count;

// Firebase variables
var database;

// Google Maps variables
var map;
var markers = [];
var infowindow;
var infowindows = [];
var content;
var contentrelinquish, contentalter;

function initArrays(c, id, light, altered, lat, lng, status, working, controllable, type, loc, desc) {
    monitor[c] = new Array();
    monitor[c] = [ctr, id, light, altered];

    pos[c] = new Array();
    pos[c] = [ctr, id, lat, lng];

    stat[c] = new Array();
    stat[c] = [ctr, id, status, working, controllable];

    info[c] = new Array();
    info[c] = [ctr, id, type, desc, loc];

    //console.log(
    //    monitor[c][1] + ", " + monitor[c][2] + ", " + monitor[c][3] + "\n" +
    //    pos[c][2] + ", " + pos[c][3] + "\n" +
    //    stat[c][2] + ", " + stat[c][3] + ", " + stat[c][4] + "\n" +
    //    info[c][2] + ", " + info[c][3] + ", " + info[c][4] + "\n"
    //);
}

var ctr = (function () {
    var ctr = 0;
    return function () {
        return ctr++;
    }
})();
var ctr2 = (function () {
    var ctr = 0;
    return function () {
        return ctr++;
    }
})();
var ctr3 = (function () {
    var ctr = 0;
    return function () {
        return ctr++;
    }
})();