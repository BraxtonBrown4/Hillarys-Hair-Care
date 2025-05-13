import { useEffect, useState } from 'react'
import './App.css'

export default function App() {
  const [stylists, setStylists] = useState([])
  const [customers, setCustomers] = useState([])
  const [AppointmentModal, setAppointmentModal] = useState(false)
  const [choosenStylistId, setChoosenStylistId] = useState(0)
  const [choosenStylist, setChoosenStylist] = useState({})

  useEffect(() => {
    const getstylists = async() => {
      const stylists = await fetch("http://localhost:5000/stylists")
      const stylistdata = await stylists.json()
      setStylists(stylistdata)
      const customers = await fetch("http://localhost:5000/customers")
      const customerdata = await customers.json()
      setCustomers(customerdata)
    }
      getstylists()
  }, [])

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
            <h2 className="title">Create a New Appointment</h2>
            <select>Choose your stylists<option value={0}>Choose your customers</option>
              {customers.map(customer => {      
                  return <option key={customer.id} value={customer.id} onClick={() => {}}>{customer.name}</option>            
              })} 
            </select>
            <select onChange={(e) => {setChoosenStylistId(e.target.value)}}> Choose your stylists<option value={0}>Choose your stylists</option>
            {stylists.map(stylist => {
                return <option key={stylist.id} value={stylist.id}>{stylist.name}</option>         
            })}
            </select>
            {choosenStylistId !== 0 && (() => {
              const stylist = stylists.find(s => s.id === parseInt(choosenStylistId));
              return stylist?.services.map(service => (
                <div>
                  <span>{service.name}</span>    
                  <input type="checkbox" name={service} id={service.id} key={service.id} />  
                  ${service.price}              
                </div>
              ))})()}
            <button className="close-button" onClick={() => setAppointmentModal(false)}>Close</button>
          </div>
        </div>
      )}
    </div>
  )
}