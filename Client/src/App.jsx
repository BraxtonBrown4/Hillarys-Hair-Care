import { useState } from 'react'
import './App.css'

export default function App() {
  const [AppointmentModal, setAppointmentModal] = useState(false)

  return (
    <div className="app-container">
      <div className="header">
        <h1 className="title">Hillary Hair Care</h1>
        <button className="create-button" onClick={() => setAppointmentModal(true)}>
          Create Appointment
        </button>
      </div>

      {AppointmentModal && (
        <div className="modal-overlay">
          <div className="modal">
            <h2>Create a New Appointment</h2>
             <select>
              <option>customers</option>
            </select>
            <select>
              <option>styliest</option>
            </select>
            <button className="close-button" onClick={() => setAppointmentModal(false)}>Close</button>
          </div>
        </div>
      )}
    </div>
  )
}