using UnityEngine;

namespace Assets.Code.Attractions
{
    public class CarouselLogic : AttractionLogic
    {
        public GameObject Body;

        public CarouselLogic()
        {
            AttractionName = "Carousel";
        }

        public override void Update()
        {
            base.Update();

            Body.transform.rotation *= Quaternion.Euler(0, Speed * Time.deltaTime, 0);
        }
    }
}