import '../css/dayPicker.css';

const Days: string[] = ["MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN"];

export default
function DayPicker({ selectedDays, onChange, validDays} : any){
    
    return (
        <div className="container row">
            { 
                Days.map((day: string) => {
                    const index = (selectedDays as any[])
                    .findIndex((sDay, pos)=> {
                        return sDay.toUpperCase().trim() === day;
                    });
                    const isSelected = (index >= 0);

                    const indexValid = ((validDays as any[]) || Days)
                    .findIndex((sDay, pos)=> {
                        return sDay.toUpperCase().trim() === day;
                    });
                    const isValid = (indexValid >= 0);

                    
                    function onClick(){
                        if(!isValid) return 
                        if(isSelected){
                            selectedDays.splice(index, 1)
                            onChange(selectedDays);
                        }else{
                            onChange([...selectedDays, day]);
                        }
                    }
                    
                    return <h2 onClick={onClick} className={`item ${ isSelected ? "selected": (isValid ? "deselected": "_disabled") }`} key={day}>{ day.trim().toUpperCase() }</h2>
                })
            }
        </div>
    )
}