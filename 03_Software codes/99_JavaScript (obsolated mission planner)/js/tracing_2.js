// https://www.geeksforgeeks.org/orientation-3-ordered-points/

var Point = (function () {
    function Point(x, y) {
        if (this.x == undefined)
            this.x = 0;
        if (this.y == undefined)
            this.y = 0;
        this.x = x;
        this.y = y;
    }
    return Point;
}())
	
function orientation(p, q, r) {   //...poly
	// console.log(...poly);
	var d0=p.x;  // for debug purpose only
	var d1=p.y;
	var d2=q.x;
	var d3=q.y;
	var d4=r.x;
	var d5=r.y;

	var val = (q.y - p.y) * (r.x - q.x) - (q.x - p.x) * (r.y - q.y);
   
	//console.log(val);
	if (val == 0)
            return 0;
        return (val > 0) ? 1 : 2;
	
  //  poly.forEach(poly => console.log(poly));
}


//-----------------------------------------------------------------

function onSegment(p, q, r) {
        if (q.x <= Math.max(p.x, r.x) && q.x >= Math.min(p.x, r.x) && q.y <= Math.max(p.y, r.y) && q.y >= Math.min(p.y, r.y)) {
            return true;
        }
        return false;
}

function doIntersect(p1, q1, p2, q2) {   //polygon[i], polygon[next], p, extreme
        var o1 = orientation(p1, q1, p2);
        var o2 = orientation(p1, q1, q2);
        var o3 = orientation(p2, q2, p1);
        var o4 = orientation(p2, q2, q1);
        if (o1 !== o2 && o3 !== o4) {  // o1 & o2 different orientations AND o3 & o4 different orientations
            return true;  // -> Segments (p1,q1) and (p2,q2) intersect
        }
        if (o1 == 0 && onSegment(p1, p2, q1)) { 	 // Special Cases (p1, q1, p2), (p1, q1, q2), (p2, q2, p1), 
            return true;							//and (p2, q2, q1) are all for collinear case
        }
        if (o2 == 0 && onSegment(p1, q2, q1)) {	// collinear case
            return true;
        }
        if (o3 == 0 && onSegment(p2, p1, q2)) {	// collinear case
            return true;
        }
        if (o4 == 0 && onSegment(p2, q1, q2)) {	// collinear case
            return true;
        }
        return false;		// the segment from 'poly' not intersect wit the point to infinite +X direction
    }

function isInside(...poly) {
        if (n < 3) {
            return false;
        }
        var extreme = new Point(10000000, p.y);
        var count = 0;
        var i = 0;
        do {
            {
                var next = (i + 1) % n;  //strat from 1 finish in 0
				var p1=poly[i],
					q1=poly[next],
					p2=p,
					q2=extreme;
				
                if (doIntersect(p1, q1, p2, q2)) {
                    if (orientation(p1, p, q1) === 0) {
                        return onSegment(p1, p, q1); // p point is on the polygons (p & r points) 
                    }
                    count++;  //Polygon Segment intersect with 'p'
                }
                i = next;
            }
        } while ((i !== 0));  // All segmenst are taken from the 'poly'
        return (count % 2 == 1);  // if =1 inside if 0 outside
    }
	
