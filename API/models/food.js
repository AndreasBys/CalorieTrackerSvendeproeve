// imports mongoose to use to create schema
import mongoose from 'mongoose'

const foodSchema = new mongoose.Schema({
    
})

// exports scheme as model to mongoose database and controllers
export const Food = mongoose.model('Food', foodSchema)