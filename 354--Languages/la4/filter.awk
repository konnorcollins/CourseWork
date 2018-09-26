BEGIN {
  FPAT = "([^,]*)|(\"[^\"]+\")"
  print "<html>\n<body>\n<table>"
  print "<tr>"
  print "<td>","Date","</td>";
  print "<td>","Subdivision","</td>";
  print "<td>","Lot","</td>";
  print "<td>","Block","</td>";
  print "<td>","Value","</td>";
  print "</tr>"
}


$3 ~ /^([Ss][Ii][Nn][Gg][Ll][Ee])/ {
  printValidTarget();
}



function printValidTarget() {
  print "<tr>"
  print "<td>",$1,"</td>";
  print "<td>",$5,"</td>";
  print "<td>",$6,"</td>";
  print "<td>",$7,"</td>";
  print "<td>",$8,"</td>";
  print "</tr>"
}

END {
  print "</table>\n</body>\n</html>";
}