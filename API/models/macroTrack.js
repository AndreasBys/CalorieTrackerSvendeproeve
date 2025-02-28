// imports mongoose to use to create schema
import mongoose from 'mongoose'

const macroTrackSchema = new mongoose.Schema({
    
})

// exports scheme as model to mongoose database and controllers
export const MacroTrack = mongoose.model('MacroTrack', macroTrackSchema)