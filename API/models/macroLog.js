// imports mongoose to use to create schema
import mongoose from 'mongoose'

const macroLogSchema = new mongoose.Schema({
    date: {
        type: Date,
        required: true
    },
    weight: {
        type: Number,
        required: true
    },
    user: {
        type: mongoose.Schema.Types.ObjectId,
        ref: "User"
    },
    food: {
        type: mongoose.Schema.Types.ObjectId,
        ref: "Food",
        required: true
    }
})

// exports scheme as model to mongoose database and controllers
const MacroLog = mongoose.model('MacroLog', macroLogSchema)
export default MacroLog;