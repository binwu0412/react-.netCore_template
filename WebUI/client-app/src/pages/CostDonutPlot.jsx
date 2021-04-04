import React, { Component } from "react";
import * as d3 from "d3";

export default class CostDonutPlot extends Component {
  componentDidUpdate(prevProps) {
    if (prevProps.benefitCostPreview !== this.props.benefitCostPreview) {
      this.graph && this.graph.remove();
      this.handlePlot();
    }
  }

  render() {
    const { benefitCostPreview } = this.props;
    return (
      <div className="donutPlot__plot-container height-100per width100per">
        <div id="costDonutPlot" className="donutPlot__plot"/>
        <div className="donutPlot__text-container">
          <div>
            <strong className="donutPlot__highlight-text">
              ${benefitCostPreview?.paycheckAfterDeduction ? benefitCostPreview.paycheckAfterDeduction.toFixed(2) : 0}
            </strong> after deduction
          </div>
          <div>{ benefitCostPreview 
            ? (benefitCostPreview.paycheckAfterDeduction / 2000).toFixed(2) + "%" 
            : "0.00%" } of total $2000 income
          </div>
          <div>
            ${benefitCostPreview?.employeeBenefitCost ? benefitCostPreview.employeeBenefitCost : 0} employee benefit cost
          </div>
          <div>
            ${benefitCostPreview?.dependendBenefitCost ? benefitCostPreview.dependendBenefitCost : 0} dependend cost
          </div>
          <div>
            ${benefitCostPreview?.employeeBenefitCostDiscount ? benefitCostPreview.employeeBenefitCostDiscount : 0} discount
          </div>
        </div>
      </div>
    );
  }

  handlePlot = () => {
    const { paycheckAfterDeduction } = this.props.benefitCostPreview;
    const percentage = paycheckAfterDeduction / 2000;
    const width = 800;
    const height = width;

    const pie = d3.pie()
      .startAngle(d => -0.5 * Math.PI)
      .endAngle(d => -0.5 * Math.PI + Math.PI * percentage)
      .sort(null)
      .value(d => d.value);
    
    const wholePie = d3.pie()
      .startAngle(d => - Math.PI)
      .endAngle(d =>  Math.PI)
      .value(d => d.value);

    const data = [{ name: "paycheckAfterDeduction", value: percentage }];
    const arcs = pie(data);
    const bases = wholePie([{ name: "", value: 1}])
    
    const arc = d3.arc()
      .innerRadius(0.61 * (Math.min(width, height) / 2 - 1))
      .outerRadius( Math.min(width, height) / 2 - 1)
      .cornerRadius(100);

    const wholeArc = d3.arc()
      .innerRadius(0.62 * (Math.min(width, height) / 2 - 1))
      .outerRadius( 0.98 * Math.min(width, height) / 2 - 1)
      .cornerRadius(100);
    
    const color = name => name === "" ? "rgba(63, 81, 181, 0.2)" : "rgba(245, 0, 87, 0.8)";

    this.graph = d3.select("#costDonutPlot")
      .append("svg")
        .attr("viewBox", [-width / 2, -height / 2, width, height]);

    let base = this.graph.append("g")
      .attr("stroke", "white");

    base.selectAll("path")
      .data(bases)
      .join("path")
        .attr("fill", d => color(d.data.name))
        .attr("d", wholeArc)

    this.graph.append("g")
        .attr("stroke", "white")
      .selectAll("path")
      .data(arcs)
      .join("path")
        .attr("fill", d => color(d.data.name))
        .attr("d", arc)
      .transition()
        .duration(1500)
        .attrTween('d', (d) => {
          const i = d3.interpolate(-Math.PI *0.5, -0.5 * Math.PI + Math.PI * percentage);
          return (t) => {
            d.endAngle = i(t); 
            return arc(d); }}); 
  }
}