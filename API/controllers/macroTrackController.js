// importing goal model
import MacroTrack from "../models/macroTrack.js"

// create method - creates a new macro goal or updates if there's one for the current day
export const createMacroTrack = async (req, res) => {
    // sets the user variable to the id of the user making the request
    req.body.user = req.user.id;

    // sets startDate to current date & time
    req.body.date = new Date();

    // logs the food
    await new MacroTrack(req.body).save()
    .then((trackLog) => {
        // returns the logged food
        res.status(201).json({ msg: 'food logged', trackLog })
    })
    .catch((error) => {
        // logs and returns status 500 if error -> food log not created
        console.log(error)
        res.status(500).json({ msg: 'unable to log food' })
    })
}