; Gaps in code are indicated by a line of periods -> '.'

; Instructions ; Bytes       ; File Addr ; Mapped Addr ; Comments
; ---------------------------------------------------------------
STA $7ECD52,X  ; 9F 52 CD 7E ; 0x00_B3B9 ; $81:B3B9    ;
INX            ; E8          ; 0x00_B3BD ; $81:B3BD    ;
INX            ; E8          ; 0x00_B3BE ; $81:B3BE    ;
CPX #$0700     ; E0 00 07    ; 0x00_B3BF ; $81:B3BF    ;
BMI $F5        ; 30 F5       ; 0x00_B3C2 ; $81:B3C2    ;
RTS            ; 60          ; 0x00_B3C4 ; $81:B3C4    ;


; New Instructions
; ---------------------------------------------------------------
JMP $EF20      ; 4C 20 EF    ; 0x00_B3C2 ; $81:B3C2    ;
; ......................................................
BMI $F5        ; 30 F5       ; 0x00_EF20 ; $81:EF20    ;

LDA #%0100     ; A9 04 00    ; 0x00_EF22 ; $81:EF22    ; set morph ball bits
STA $09A4      ; 8D A4 09    ; 0x00_EF25 ; $81:EF25    ;
STA $09A2      ; 8D A2 09    ; 0x00_EF28 ; $81:EF28    ;

; set save location

RTS            ; 60          ; 0x00_EF2B ; $81:EF2B    ;