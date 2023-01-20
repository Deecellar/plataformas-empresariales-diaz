<script>
  import { onMount } from "svelte";
  import Chart from "chart.js"
  
  onMount(() => {
    
    var chart : HTMLCanvasElement  = document.getElementById("myChart") as HTMLCanvasElement;
    var ctx : CanvasRenderingContext2D = chart.getContext("2d") as CanvasRenderingContext2D;
    new Chart(ctx, {
      // The type of chart we want to create
      type: "line",

      // The data for our dataset
      data: {
        labels: [
          "January",
          "February",
          "March",
          "April",
          "May",
          "June",
          "July"
        ],
        datasets: [
          {
            label: "My First dataset",
            backgroundColor: "rgb(255, 99, 132)",
            borderColor: "rgb(255, 99, 132)",
            data: [0, 10, 5, 2, 20, 30, 45]
          }
        ]
      },

      // Configuration options go here
      options: {}
    });
  });
</script>

<style>
  /* your styles go here */
</style>

<canvas id="myChart" />
