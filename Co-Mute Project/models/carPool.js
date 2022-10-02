const mongoose = require('mongoose');

const CarPoolSchema = new mongoose.Schema({
    id: {
        type: String,
        required: false
    },
    departure_time: {
        type: String,
        required: true
    },
    arrival_time: {
        type: String,
        required: true
    },
    origin: {
        type: String,
        required: true
    },
    days_available: {
        type: Array,
        required: true
    },
    destination: {
        type: String,
        required: true
    },
    available_seats: {
        type: Number,
        required: true
    },
    owner: {
        type: String,
        required: true
    },
    notes: {
        type: String,
        required: false
    },
    dateJoined: {
        type: String,
        required: false
    }
}, {timestamps: true})

const carPoolModel = mongoose.model('CarPool_opportunitie', CarPoolSchema);

module.exports = carPoolModel;