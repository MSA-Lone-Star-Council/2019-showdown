using System;
using Common.Common.Models;
using Foundation;
using MapKit;
using UIKit;

namespace Client.iOS 
{
    public class MapDelegate : MKMapViewDelegate
    {
        protected string annotationIdentifier = "BasicAnnotation";
        UIButton detailButton;
        UIViewController parent;
        Event mappedEvent;

        public MapDelegate(UIViewController parent, Event mappedEvent)
        {
            this.parent = parent;
            this.mappedEvent = mappedEvent;
        }
        /// <summary>
        /// This is very much like the GetCell method on the table delegate
        /// </summary>
        public override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            // try and dequeue the annotation view
            MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(annotationIdentifier);
            // if we couldn't dequeue one, create a new one
            if (annotationView == null) 
            {
                annotationView = new MKPinAnnotationView(annotation, annotationIdentifier);
            }                
            else // if we did dequeue one for reuse, assign the annotation to it
            {
                annotationView.Annotation = annotation;
            }

            // configure our annotation view properties
            annotationView.CanShowCallout = true;
            (annotationView as MKPinAnnotationView).AnimatesDrop = true;
            (annotationView as MKPinAnnotationView).PinColor = MKPinAnnotationColor.Green;
            annotationView.Selected = true;
            // you can add an accessory view, in this case, we'll add a button on the right, and an image on the left
            detailButton = UIButton.FromType(UIButtonType.DetailDisclosure);
            detailButton.TouchUpInside += (s, e) => {
                //Navigate to Location 
                var path = $"https://maps.apple.com/?daddr={ mappedEvent.Location.Address }";
                NSUrl url = new NSUrl(System.Uri.EscapeUriString(path));
                if (UIApplication.SharedApplication.CanOpenUrl(url))
                {
                    UIApplication.SharedApplication.OpenUrl(url);
                }
            };
            annotationView.RightCalloutAccessoryView = detailButton;
            //annotationView.LeftCalloutAccessoryView = new UIImageView(UIImage.FromBundle("29_icon.png"));
            return annotationView;
        }

        // as an optimization, you should override this method to add or remove annotations as the
        // map zooms in or out.
        public override void RegionChanged(MKMapView mapView, bool animated) { }
    }
}
