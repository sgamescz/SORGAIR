<!doctype html>
<html>
<head>
<meta http-equiv='Content-language' content='cs'>
<meta http-equiv='content-type' content='text/html;charset=UTF-8'>
<!--<link rel='stylesheet' href='https://www.w3schools.com/w3css/4/w3.css'>
<link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css'>-->
<link rel='stylesheet' type='text/css' href='style.css'>
<script>
    function showDiv() {
   document.getElementById('info').style.display = 'block';
   document.getElementById('info_min').style.display = 'none';
}
function hideDiv() {
   document.getElementById('info').style.display = 'none';
   document.getElementById('info_min').style.display = 'block';
}
    
function startTime() {
  var today = new Date();
  var date = new Date();

  var h = today.getHours();
  var m = today.getMinutes();
  var s = today.getSeconds();
  m = checkTime(m);
  s = checkTime(s);
  document.getElementById('cas').innerHTML = 
  h + ":" + m + ":" + s;
  var t = setTimeout(startTime, 500);
}
function checkTime(i) {
  if (i < 10) {i = "0" + i};  // add zero in front of numbers < 10
  return i;
}

function printPage() {
  window.print();
}


    </script>
</head>

