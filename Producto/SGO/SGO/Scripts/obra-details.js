



var options = {
    offText: "%",
    onText: "$",
    animate: true,
    offColor: 'warning',
    onColor: 'danger'
};
$("#ckbEnPesos").bootstrapSwitch(options);

var CrearGraficoBarras = function (data) {
    d3.select(".graficoBarras").selectAll("svg").remove();
    d3.select(".graficoBarras")
        .append("svg")        
        .attr("height", "100%")
        .attr("width", "100%")
        .selectAll("rect")
        .data(data).enter()
        .append("rect")
            .attr("class", "bar")
            .attr("height", function (d, i) { return (d * 1.5) })
            .attr("width", "40")
            .attr("x", function (d, i) { return (i * 60) + 25 })
            .attr("y", function (d, i) { return 150 - (d * 1.5) });
};
var CrearGraficoTorta = function (data) {
    var width = 250,
        height = 250,
        radius = Math.min(width, height) / 2;
    var color = d3.scaleOrdinal()
        .range(["#F0AD4E", "#D9534E", "#F56C4E"])
    var pie = d3.pie()
        .value(function (d) { return d.valor; })(data);
    var arc = d3.arc()
        .outerRadius(radius - 10)
        .innerRadius(0);
    var labelArc = d3.arc()
        .outerRadius(radius - 40)
        .innerRadius(radius - 40);
    var svg = d3.select("#graficoTorta")
        .append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");
    var g = svg.selectAll("arc")
        .data(pie)
        .enter().append("g")
        .attr("class", "arc");
    g.append("path")
        .attr("d", arc)
        .style("fill", function (d) { return color(d.data.estado); });
    g.append("text")
        .attr("transform", function (d) { return "translate(" + labelArc.centroid(d) + ")"; })
        .text(function (d) { return d.data.estado; })
        .style("fill", "#fff");
};