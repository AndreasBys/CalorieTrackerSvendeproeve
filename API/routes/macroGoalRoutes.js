// importing express module
import express from 'express'

// importing authentcate 
import { authenticate } from '../middleware/authenticator.js';

// importing controllers
import { createMacroGoal, getCurrentGoal } from '../controllers/macroGoalController.js'

// creates new router from express module
const router = express.Router();

// defining post routes
router.post('/', authenticate, createMacroGoal);
router.get('/', authenticate, getCurrentGoal);

// exporting router as default
export default router;
