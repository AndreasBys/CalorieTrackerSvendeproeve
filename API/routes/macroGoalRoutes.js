// importing express module
import express from 'express'

// importing authentcate 
import { authenticate } from '../middleware/authenticator.js';

// importing controllers
import { createMacroGoal, getCurrentGoal, getGoalsBetweenDates } from '../controllers/macroGoalController.js'

// creates new router from express module
const router = express.Router();

// defining routes
router.post('/', authenticate, createMacroGoal);
// get route depending on if theres query params present
router.get('/', authenticate, (req, res, next) => {
    if (Object.keys(req.query).length > 0) {
        getGoalsBetweenDates(req, res, next);
    } else {
        getCurrentGoal(req, res, next);
    }
});

// exporting router as default
export default router;
