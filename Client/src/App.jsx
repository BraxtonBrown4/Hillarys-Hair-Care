import { useEffect, useState } from 'react'
import './App.css'

export default function App() {
  const [stylists, setStylists] = useState([])
  const [customers, setCustomers] = useState([])
  const [AppointmentModal, setAppointmentModal] = useState(false)
  const [chosenStylistId, setChosenStylistId] = useState(0)
  const [chosenCustomerId, setChoosenCustomerId] = useState(0)
  const [services, setServices] = useState([])
  const [appointments, setAppointments] = useState([])
  const [showModal, setShowModal] = useState(false)


  const getData = async () => {
    const stylists = await fetch("http://localhost:5000/stylists")
    const stylistdata = await stylists.json()
    setStylists(stylistdata)

    const customers = await fetch("http://localhost:5000/customers")
    const customerdata = await customers.json()
    setCustomers(customerdata)

    const appointments = await fetch("http://localhost:5000/appointments")
    const appointmentdata = await appointments.json()
    setAppointments(appointmentdata)
  }
  useEffect(() => {
    getData()
  }, [])

  const handleCheckChange = (e) => {
    const { checked, value } = e.target
    if (checked) {
      setServices((prev) => [...prev, value])
    } else {
      setServices((prev) => prev.filter((service) => service !== value))
    }
  }

  const handleSchedule = async () => {
    const now = new Date()
    now.setHours(now.getHours() + 1, 0, 0, 0)

    if (chosenCustomerId == 0 || chosenStylistId == 0) {
      alert("please make sure all fields are selected")
      return
    }
    const createdRes = await fetch("http://localhost:5000/appointments", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        Date: now,
        stylistId: chosenStylistId,
        customerId: chosenCustomerId,
      })
    })

    const createdAppointment = await createdRes.json()

    services.map(s => {
      fetch("http://localhost:5000/appointmentServices", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          appointmentId: createdAppointment.id,
          serviceId: parseInt(s)
        })
      })
    })

    setChosenStylistId(0)
    setChoosenCustomerId(0)
    setServices([])
    setAppointmentModal(false)
    getData()
  }

  return (
    <div className="app-container">
      <div className="header">
        <h1 className="title">Hillary Hair Care</h1>
        <button className="create-button" onClick={() => setAppointmentModal(true)}>
          Create Appointment
        </button>
      </div>

      <button className="open-button" onClick={() => setShowModal(true)}>
        View Appointments
      </button>
      
       {showModal && (
        <div className="modal-overlay" onClick={() => setShowModal(false)}>
          <div className="modal-content" onClick={(e) => e.stopPropagation()}>
            <h2 className="modal-title">Appointments</h2>
            <div className="appointments-container">
              {appointments.map((a) => (
                <div key={a.id} className="appointment-card">
                  <h3>Stylist: {a.stylist.name}</h3>
                  <h3>Customer: {a.customer.name}</h3>
                  <p>
                    Scheduled:{" "}
                    {new Date(a.date).toLocaleTimeString([], {
                      hour: '2-digit',
                      minute: '2-digit',
                      hour12: true,
                    })}
                  </p>
                  <h4>Services</h4>
                  {a.services.map((s, idx) => (
                    <p key={idx}>{s.name} – ${s.price}</p>
                  ))}
                  <h3>Total: ${25 + a.services.reduce((t, s) => t + s.price, 0)}</h3>
                </div>
              ))}
            </div>
            <button className="close-button" onClick={() => setShowModal(false)}>Close</button>
          </div>
        </div>
      )}

      {AppointmentModal && (
        <div className="modal-overlay">
          <div className="modal">
            <h2 className="title">Create a New Appointment</h2>
            <select onChange={(e) => { setChoosenCustomerId(e.target.value) }}>Choose your stylists<option value={0}>Choose your customers</option>
              {customers.map(customer => {
                return <option key={customer.id} value={customer.id} onClick={() => { }}>{customer.name}</option>
              })}
            </select>
            <select onChange={(e) => { setChosenStylistId(e.target.value) }}> Choose your stylists<option value={0}>Choose your stylists</option>
              {stylists.map(stylist => {
                return <option key={stylist.id} value={stylist.id}>{stylist.name}</option>
              })}
            </select>
            {chosenStylistId !== 0 && (() => {
              const stylist = stylists.find(s => s.id === parseInt(chosenStylistId));
              return stylist?.services.map(service => (
                <div>
                  <span>{service.name}</span>
                  <input type="checkbox" value={service.id} id={service.id} key={service.id} onChange={handleCheckChange} />
                  ${service.price}
                </div>
              ))
            })()}
            <button className="close-button" onClick={handleSchedule}>Schedule</button>
            <button className="close-button" onClick={() => setAppointmentModal(false)}>Close</button>
          </div>
        </div>
      )}
    </div>
  )
}