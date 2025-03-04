function updateClock() {
    const now = new Date();
    let hours = now.getHours();
    const minutes = now.getMinutes();
    const seconds = now.getSeconds();
    const ampm = hours >= 12 ? 'PM' : 'AM';
  
    // Convert to 12-hour format
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
  
    const timeString = `${hours.toString().padStart(2, '0')}:${minutes
     .toString()
     .padStart(2, '0')}:${seconds.toString().padStart(2, '0')} ${ampm}`;
  
    document.getElementById('clock').textContent = timeString;
   }
  
   // Update the clock every second
   setInterval(updateClock, 1000);
  