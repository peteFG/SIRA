import { Coordinate } from 'ol/coordinate';
import Feature from 'ol/Feature';
import { Circle, Point } from 'ol/geom';
import VectorLayer from 'ol/layer/Vector';
import { fromLonLat } from 'ol/proj';
import VectorSource from 'ol/source/Vector';
import { Fill, Icon, Stroke, Style, Text } from 'ol/style';

export const defaultCircleRadius = 20;

export function getCircleFeatureStyle(coords: number[]) {
    const circleFeature = new Feature({
        geometry: new Circle(fromLonLat(coords), defaultCircleRadius),
      });
      circleFeature.setStyle(
        new Style({
          renderer(coordinates, state) {
            const [[x, y], [x1, y1]] = coordinates as Coordinate[];
            const ctx = state.context;
            const dx = x1 - x;
            const dy = y1 - y;
            const radius = Math.sqrt(dx * dx + dy * dy);
  
            const innerRadius = 0;
            const outerRadius = radius * 1.4;
  
            const gradient = ctx.createRadialGradient(
              x,
              y,
              innerRadius,
              x,
              y,
              outerRadius
            );
            gradient.addColorStop(0, 'rgba(255,0,0,0)');
            gradient.addColorStop(0.6, 'rgba(255,0,0,0.2)');
            gradient.addColorStop(1, 'rgba(255,0,0,0.8)');
            ctx.beginPath();
            ctx.arc(x, y, radius, 0, 2 * Math.PI, true);
            ctx.fillStyle = gradient;
            ctx.fill();
  
            ctx.arc(x, y, radius, 0, 2 * Math.PI, true);
            ctx.lineWidth = 10;
            ctx.strokeStyle = 'rgba(255,0,0,1)';
            ctx.stroke();

            // ctx.font = "50px serif";
            // ctx.fillText("Hello world", x, y);
          },
        })
      );
      return circleFeature;
}

export function getVectorLayer(circleFeature: Feature<Circle>) {
    return new VectorLayer({
        source: new VectorSource({
          features: [circleFeature],
        }), 
      })
}

export function getMarkerLayer(coord: number[], icon: string = "map-marker.png") {
  var feature =  new Feature({
    geometry: new Point(fromLonLat(coord))
});
feature.setStyle(new Style({
  image: new Icon({
    anchor: [0.5, 46],
    anchorXUnits: 'fraction',
    anchorYUnits: 'pixels',
    src: '/assets/marker/'+icon
  })
}))
  return new VectorLayer({
    source: new VectorSource({
        features: [
           feature
        ],
    })
  });
}