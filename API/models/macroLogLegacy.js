// imports mongoose to use to create schema
import mongoose from 'mongoose'

const macroLogLegacyScheme = new mongoose.Schema({
    date: {
        type: Date,
        required: true,
    },
    user: {
        type: mongoose.Schema.Types.ObjectId,
        ref: "User"
    },
    calories: {
        type: Number,
        required: false
    },
    carbonhydrates: {
        type: Number,
        required: false
    },
    protein: {
        type: Number,
        required: false
    },
    fat: {
        type: Number,
        required: false
    }
})

// exports scheme as model to mongoose database and controllers
const MacroLogLegacy = mongoose.model('MacroLogLegacy', macroLogLegacyScheme)
export default MacroLogLegacy;