using UnityEngine;

namespace Assets.Code.Attractions
{
    public class CarouselLogic : AttractionLogic
    {
        public override void Update()
        {
            base.Update();

            transform.rotation *= Quaternion.Euler(0, _speed * Time.deltaTime, 0);
        }
    }
}