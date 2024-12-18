using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.EventSystems;
//using UnityEditor.MemoryProfiler;
using static GameEvents;
using static System.Net.Mime.MediaTypeNames;
//using UnityEditor.Experimental.GraphView;
//using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;


public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public TextMeshProUGUI numberText;

    public GameObject number_text;
    public List<GameObject> number_notes;
    private bool note_active;

    private int number_ = 0;
    private int correct_number_ = 0;

    private bool selected_ = false;
    private int square_index_ = -1;

    private bool has_default_value_ = false;
    private bool has_wrong_value_ = false;

    public bool IsCorrectNumberSet()
    {
        return number_ == correct_number_;
    }
    public bool HasWrongValue() {  return has_wrong_value_; }

    public void SetHasDefaultValue(bool has_default) { has_default_value_ =  has_default;}
    public bool GetHasDefaultValue() {  return has_default_value_; }
    public bool IsSelected() {  return selected_; }
    public void SetSquareIndex(int index) 
    {
        square_index_ = index;
    }
    public void SetCorrectNumber(int number)
    {
        correct_number_ = number;
        has_wrong_value_ = false;
    }

    public void SetCorrectNumber()
    {
        number_ = correct_number_;
        SetNoteNumberValue(0);
        //DisplayText();
        if (numberText != null)
        {
            if (number_ == 0)
                numberText.text = " ";
            else
                numberText.text = number_.ToString();
        }
        else
        {
            Debug.LogError("NumberText is not assigned!");
        }
    }

    void Startt()
    {
        selected_ = false;
        note_active = false;

        SetNoteNumberValue(0);
    }


    public List<string> GetSquareNotes()
    {
        List<string> notes = new List<string>();
        foreach (var number in number_notes)
        {
            notes.Add(number.GetComponent<TextMeshProUGUI>().text);
        }
        return notes;
    }
    private void SetClearEmptyNotes()
    {
        foreach (var number in number_notes)
        {
            if (number.GetComponent<TextMeshProUGUI>().text == "0")
                number.GetComponent<TextMeshProUGUI>().text = " ";
        }
    }
    private void SetNoteNumberValue(int value)
    {
        foreach (var number in number_notes)
        {
            if (value <= 0)
                number.GetComponent<TextMeshProUGUI>().text = " ";
            else
                number.GetComponent<TextMeshProUGUI>().text = value.ToString();
        }
    }
    private void SetNoteSingleNumberValue(int value, bool force_update = false)
    {
        if (note_active == false && force_update == false)
            return;
        if (value <= 0)
            number_notes[value - 1].GetComponent<TextMeshProUGUI>().text = " ";
        else
        {
            if (number_notes[value - 1].GetComponent<TextMeshProUGUI>().text == " " || force_update)
                number_notes[value - 1].GetComponent<TextMeshProUGUI>().text = value.ToString();
            else
            {
                number_notes[value - 1].GetComponent<TextMeshProUGUI>().text = " ";
            }
        }
    }
    public void SetGridNotes(List<int> notes)
    {
        foreach (var note in notes)
        {
            SetNoteSingleNumberValue(note, true);
        }
    }
    public void OnNotesActive(bool active)
    {
        note_active = active;
    }

    public void DisplayText()
    {
        if (number_ == 0)
        {
            number_text.GetComponent<TextMeshProUGUI>().text = " "; // Hiển thị khoảng trống
        }
        else
        {
            number_text.GetComponent<TextMeshProUGUI>().text = number_.ToString();
        }
    }


    public void SetNumber(int number)
    {
        number_ = number;
        //DisplayText();
        if (numberText != null)
        {
            if (number == 0)
                numberText.text = " ";
            else
                numberText.text = number.ToString();
        }
        else
        {
            Debug.LogError("NumberText is not assigned!");
        }

       

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected_ = true;
        GameEvents.SquareSelectedMethod(square_index_);
    }

    public void OnSubmit(BaseEventData eventData)
    {

    }

    private void OnEnable() 
    {
        GameEvents.OnUpdataSquareNumber += OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
        GameEvents.OnNotesActive += OnNotesActive;
        GameEvents.OnClearNumber += OnClearNumber;
    }
    private void OnDisable()
    {
        GameEvents.OnUpdataSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected -= OnSquareSelected;
        GameEvents.OnNotesActive -= OnNotesActive;
        GameEvents.OnClearNumber -= OnClearNumber;
    }

    public void OnClearNumber()
    {
        if(selected_ && !has_default_value_)
        {
            number_ = 0;
            has_wrong_value_ = false;
            SetSqaureColour(Color.white);
            SetNoteNumberValue(0);
            //DisplayText();
            if (numberText != null)
            {
                numberText.text = " "; // Hiển thị khoảng trống
            }
            else
            {
                Debug.LogError("NumberText is not assigned!");
            }
        }
    }

    public void OnSetNumber(int number)
    {
        if (selected_ && has_default_value_ == false)
        {

            if (note_active == true && has_wrong_value_ == false)
            {
                SetNoteSingleNumberValue(number);
            }
            else if (note_active == false)
            {
                SetNoteNumberValue(0);
                SetNumber(number);

                if (number_ != correct_number_)
                {
                    has_wrong_value_ = true;
                    var colors = this.colors;
                    colors.normalColor = Color.red;
                    this.colors = colors;
                    GameEvents.OnWrongNumberMethod();
                }
                else
                {
                    has_wrong_value_ = false;
                    has_default_value_ = true;
                    var colors = this.colors;
                    colors.normalColor = Color.white;
                    this.colors = colors;
                }
            }
        }

        
    }

    public void OnSquareSelected(int square_index)
    {
        if (square_index_ != square_index)
        {
            selected_ = false;
        }
    }

    public void SetSqaureColour(Color col)
    {
        var colors = this.colors;
        colors.normalColor = col;
        this.colors = colors;
    }

}

