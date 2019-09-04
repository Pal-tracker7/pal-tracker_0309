using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public  class TimeEntryController : ControllerBase
    {
        public ITimeEntryRepository _timeEntryRepository { get; set; }
        public TimeEntryController(ITimeEntryRepository timeEntryRepository) => _timeEntryRepository = timeEntryRepository;
        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult  Read(int id)
        {
            var timeEntry= _timeEntryRepository.Find(id);
            if(timeEntry.Id==null)
            {
              return NotFound();
            }
          
           return new OkObjectResult(timeEntry);
        }
        [HttpDelete("{id}")]
        public IActionResult  Delete(int id) 
        {
            if(!_timeEntryRepository.Contains(id))
            {
              return NotFound();
            }
           _timeEntryRepository.Delete(id);
           return NoContent();
        }
        [HttpGet]
        public IActionResult  List()=>new OkObjectResult(_timeEntryRepository.List());
       [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
            return _timeEntryRepository.Contains(id) ? (IActionResult) Ok(_timeEntryRepository.Update(id, timeEntry)) : NotFound();
        }
         [HttpPost]
        public IActionResult  Create ([FromBody] TimeEntry timeEntry)
        {
           //return CreatedAtRoute("GetTimeEntry",new {id = timeEntry.Id},_timeEntryRepository.Create(timeEntry));

           var createdTimeEntry = _timeEntryRepository.Create(timeEntry);

            return CreatedAtRoute("GetTimeEntry", new {id = createdTimeEntry.Id}, createdTimeEntry);
        }  
       
    }
}